namespace LibraCore.GCommon
{
    public static class OutputMessages
    {
        public static class Book
        {
            public const string TitleRequired = "Book title is required.";
            public const string DescriptionRequired = "Please provide a book description.";
            public const string GenreRequired = "Please select a genre from the list.";
            public const string PriceRange = "Price must be a positive value.";
            public const string ReleaseDateRequired = "Please select a release date.";
            public const string AuthorRequired = "Please select an author from the list.";
            public const string ImageUrlInvalid = "The Image URL is not valid.";

            public const string CrudBookFailureMessage = "An error while {0} book. Please try again later.";

            public const string PersistFailureMessage = "An error occurred while saving the changes. Please try again.";
            public const string GeneralError = "Something went wrong. Please try again.";

        }
    }
}
