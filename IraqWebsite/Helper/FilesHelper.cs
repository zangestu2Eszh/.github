namespace IraqWebsite.Helper
{
    public class FilesHelper
    {
        //public static async Task<string> ValdiateFilesAsync(IFormFile? Attachment)
        //{
        //    if (Attachment is not null)
        //    {
        //        try
        //        {
        //            var ext = new List<string> { ".jpg", ".png", ".jpeg", ".webp", ".ico" };

        //            var fileValidate = ext.Contains(Path.GetExtension(Attachment.FileName));
        //            if (fileValidate is false)
        //            {
        //                return "Only JPG, PNG, and JPEG file types are allowed.";
        //            }

        //            // Check file size
        //            var fileSizeLimit = 5 * 1024 * 1024; // 5 MB
        //            if (Attachment.Length > fileSizeLimit)
        //            {
        //                return "The file exceeds the maximum size limit of 5 MB.";
        //            }

        //            if (Attachment.Length > 0)
        //            {
        //                if (Attachment.FileName.Contains(","))
        //                    return null;


        //                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images\" , Attachment.FileName);

        //                using (var stream = System.IO.File.Create(filePath))
        //                {
        //                    await Attachment.CopyToAsync(stream);
        //                }
        //            }
        //            return "Successful Saved";
        //        }
        //        catch
        //        {
        //            return "Invalid File Type";
        //        }
        //    }

        //    return "";
        //}

        public static async Task<string> ValdiateFilesAsync(IFormFile? Attachment, string wwwroot)
        {
            if (Attachment is not null)
            {
                try
                {
                    var ext = new List<string> { ".jpg", ".png", ".jpeg", ".webp", ".ico",".gif" };

                    var fileValidate = ext.Contains(Path.GetExtension(Attachment.FileName));
                    if (fileValidate is false)
                    {
                        return "Only JPG, PNG, and JPEG file types are allowed.";
                    }

                    // Check file size
                    var fileSizeLimit = 5 * 1024 * 1024; // 5 MB
                    if (Attachment.Length > fileSizeLimit)
                    {
                        return "The file exceeds the maximum size limit of 5 MB.";
                    }

                    if (Attachment.Length > 0)
                    {
                        if (Attachment.FileName.Contains(","))
                            return null;



                        var filePath = Path.Combine(wwwroot, @"Images", Attachment.FileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await Attachment.CopyToAsync(stream);
                        }
                    }
                    return "Successful Saved";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

            return "";
        }
        public static async Task<string> ValdiateAudioAsync(IFormFile? Attachment, string wwwroot)
        {
            if (Attachment is not null)
            {
                try
                {
                    var ext = new List<string> { ".mp3", ".wav", ".aac"};

                    var fileValidate = ext.Contains(Path.GetExtension(Attachment.FileName));
                    if (fileValidate is false)
                    {
                        return "Only MP3, WAV, and AAC file types are allowed.";
                    }

                    // Check file size
                    //var fileSizeLimit = 5 * 1024 * 1024; // 5 MB
                    //if (Attachment.Length > fileSizeLimit)
                    //{
                    //    return "The file exceeds the maximum size limit of 5 MB.";
                    //}

                    if (Attachment.Length > 0)
                    {
                        if (Attachment.FileName.Contains(","))
                            return null;



                        var filePath = Path.Combine(wwwroot, @"Audio", Attachment.FileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await Attachment.CopyToAsync(stream);
                        }
                    }
                    return "Successful Saved";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }

            return "";
        }
    }
}
