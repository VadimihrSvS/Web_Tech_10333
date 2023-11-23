using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab_WT_Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNet_Projects.Controllers;

public class ImageController : Controller
{
    UserManager<ApplicationUser> _userManager;
    IWebHostEnvironment _env;
    public ImageController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _env = env;
    }
    public async Task<FileResult> GetAvatar()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user.AvatarImage != null)
            return File(user.AvatarImage, "image/...");
        else
        {
            var avatarPath = "/Images/anonymous.png";

            return File(_env.WebRootFileProvider
            .GetFileInfo(avatarPath).CreateReadStream(), "image/...");
        }
    }
}


