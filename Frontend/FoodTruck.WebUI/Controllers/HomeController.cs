using System.Security.Claims;
using FoodTruck.WebUI.Interfaces;
using FoodTruck.WebUI.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodTruck.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly ICartRepository _cartRepository;
        public HomeController(IFoodRepository foodRepository, ITruckRepository truckRepository, ICartRepository cartRepository)
        {
            _foodRepository = foodRepository;
            _truckRepository = truckRepository;
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<FoodDto>? list = null;

            //var truckCount = await _truckRepository.GetTruckCountAsync();

            //ViewBag.truckCount = truckCount.Result;

            ResponseDto? response = await _foodRepository.GetAllFoodsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<FoodDto>>(Convert.ToString(response.Result));
            }

            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> FoodDetails(int foodId)
        {
            FoodDto? food = null;

            ResponseDto? response = await _foodRepository.GetFoodByIdAsync(foodId);

            if (response != null && response.IsSuccess)
            {
                food = JsonConvert.DeserializeObject<FoodDto>(Convert.ToString(response.Result));
            }

            else
            {
                TempData["error"] = response?.Message;
            }

            return View(food);
        }

        [Authorize]
        [HttpPost]
        [ActionName("FoodDetails")]
        public async Task<IActionResult> FoodDetails(FoodDto foodDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(x => x.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
                }
            };

            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = foodDto.Count,
                ProductId = foodDto.FoodId,
            };

            List<CartDetailsDto> cartDetailsDtos = new() { cartDetails };
            cartDto.CartDetails = cartDetailsDtos;

            cartDto.CartHeader.Email = User.Claims.Where(x => x.Type == JwtClaimTypes.Email)?.FirstOrDefault()?.Value;
            cartDto.CartHeader.Phone = "1111111";
            cartDto.CartHeader.Name = User.Claims.Where(x => x.Type == ClaimTypes.Name)?.FirstOrDefault()?.Value;

            ResponseDto? response = await _cartRepository.UpsertCartAsync(cartDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item has been added to Shopping Cart";
                return RedirectToAction(nameof(Index));
            }

            else
            {
                TempData["error"] = response?.Message;
            }

            return View(foodDto);
        }
    }
}
