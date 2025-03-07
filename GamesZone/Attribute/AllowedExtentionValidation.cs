﻿

namespace GamesZone.Attribute
{
	public class AllowedExtentionValidation: ValidationAttribute
	{
		private readonly string _allowedExtentions;

		public AllowedExtentionValidation(string AllowedExtentions)
        {
			_allowedExtentions = AllowedExtentions;
		}

		protected override ValidationResult? IsValid
			(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file != null)
			{
				var extention = Path.GetExtension(file.FileName);
				var isAllowed = _allowedExtentions.Split(',').Contains(extention,StringComparer.OrdinalIgnoreCase);
				if (!isAllowed)
				{
					return new ValidationResult($"" +
						$"This extention is not allowed, " +
						$"allowed extentions are {_allowedExtentions}");
				}
			}
			return ValidationResult.Success;
		}

	}
}
