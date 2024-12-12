using FoodTruck.WebUI.Interfaces;
using FoodTruck.WebUI.Models;
using FoodTruck.WebUI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FoodTruck.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        public CartController(ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailId)
        {
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartRepository.RemoveFromCartAsync(cartDetailId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.cartHeader.email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartRepository.EmailCart(cart);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly.";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            CartDto cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.cartHeader.phone = cartDto.cartHeader.phone;
            cart.cartHeader.email = cartDto.cartHeader.email;
            cart.cartHeader.name = cartDto.cartHeader.name;

            var response = await _orderRepository.CreateOrder(cart);

            OrderHeaderDto? orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderWrapper>(Convert.ToString(response.Result))?.OrderHeaderDto;

            if (response != null && response.IsSuccess)
            {
                //get stripe session and redirect to stripe to place order
                //
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "Cart/Confirmation?orderId=" + orderHeaderDto.OrderId,
                    CancelUrl = domain + "Cart/Checkout",
                    orderHeaderDto = orderHeaderDto
                };
                var stripeResponse = await _orderRepository.CreateStripeSession(stripeRequestDto);

                var stripeResponseResult = JsonConvert.DeserializeObject<StripeResponseWrapper>(
                                           Convert.ToString(stripeResponse.Result)
                                           )?.StripeRequestDto;

                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);

                return new StatusCodeResult(303);
            }
            return View();
        }

        public async Task<IActionResult> Confirmation(int orderId)
        {
            ValidateStripeDto validateStripeDto = new ValidateStripeDto { orderId = orderId, truckId = 0 };
            ResponseDto? response = await _orderRepository.ValidateStripeSession(validateStripeDto);
            if (response != null && response.IsSuccess)
            {
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderWrapper>(Convert.ToString(response.Result))?.OrderHeaderDto;
                if (orderHeader.OrderStatusId == SD.int_Status_Approved)
                {
                    return View(orderId);
                }
                TempData["success"] = " updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View(orderId);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon()
        {
            var couponCode = " ";
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartRepository.ApplyCouponAsync(userId, couponCode);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartRepository.ApplyCouponAsync(userId, cartDto.cartHeader.couponCode);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto? response = await _cartRepository.GetCartByUserIdAsync(userId);
            if (response != null && response.IsSuccess)
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new CartDto();
        }
    }
}