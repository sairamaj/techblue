using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using web.Models;
using web.Extensions;

namespace web.Controllers
{
    [Authorize]
    public class RewardController : Controller
    {
        IClassRepository _classRepository;
        public RewardController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                var rewards = await _classRepository.GetRewards(this.GetId());
                return View(rewards);
            }
            catch (TaskCanceledException te)
            {
                return View("TimeoutError");
            }
        }

        [AllowAnonymous]
        public IActionResult HowRewardsWork()
        {
            return View();
        }
    }
}
