using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{

    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _cartRepository = new CartRepository();

        [HttpGet]
        [Route("list")]
        [ProducesResponseType(typeof(ListResponse<CartModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCartItems(int UserId,int pageIndex = 1, int pageSize = 10, string keyword="")
        {
            ListResponse<Cart> carts = _cartRepository.GetCartItems(UserId, pageIndex, pageSize, keyword);

            ListResponse<CartBook> listResponse = new ListResponse<CartBook>()
            {
                records = carts.records.Select(c => new CartBook(c)),
                TotalRecords = carts.TotalRecords,
            };

            return Ok(listResponse);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult AddCart(CartModel model)
        {
            if (model == null)
                return BadRequest();
            
            Cart cart = new Cart()
            {
                //Id = model.Id,
                Quantity = 1,
                Bookid = model.BookId,
                Userid = model.UserId,
                //Quantity = model.Quantity
            };
            cart = _cartRepository.AddCart(cart);

            if (cart == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Already Item Exist in Cart");
            }

            return Ok(new CartModel(cart));
        }
    
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(CartModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult UpdateCart(CartModel model)
        {
            if (model == null)
                return BadRequest();

            Cart cart = new Cart()
            {
                Id = model.Id,
                //Quantity = 1,
                Bookid = model.BookId,
                Userid = model.UserId,
                Quantity = model.Quantity
            };
            cart = _cartRepository.UpdateCart(cart);

            return Ok(new CartModel(cart));
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]

        public IActionResult DeleteCart(int id)
        {
            if (id == 0)
                return BadRequest();

            bool response = _cartRepository.DeleteCart(id);
            return Ok(response);
        }


    }
}
