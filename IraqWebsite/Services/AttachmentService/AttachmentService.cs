using AutoMapper;
using IraqWebsite.Data;
using IraqWebsite.Helper;
using IraqWebsite.Models;

namespace IraqWebsite.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AttachmentService(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        //public async Task<string> ourPortfolioImageAsync(Guid id,List<IFormFile> files)
        //{
        //    foreach (var file in files)
        //    {
        //        var img = await FilesHelper.ValdiateFilesAsync(file, _webHostEnvironment.WebRootPath);
        //        if (img != "Successful Saved")
        //            return img;
        //        OurPortfolioAttachment attachment = new()
        //        {
        //            Id = Guid.NewGuid(),
        //            Img = "/Images/" + file.FileName,
        //        };
        //        await _context.OurPortfolioAttachment.AddAsync(attachment);
        //    }
        //    return "Successful Saved";
        //}
    }
}
