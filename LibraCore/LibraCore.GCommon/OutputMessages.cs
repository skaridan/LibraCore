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

            public const string AddBookSuccessMessage = "Book was added successfully.";
            public const string EditBookSuccessMessage = "Book was updated successfully.";
            public const string DeleteBookSuccessMessage = "Book was deleted successfully.";
        }

        public static class Favorite
        {
            public const string BookAlreadyInFavoritesMessage = "The book {0} is already in user {1} favorites.";
            public const string AddToFavoritesFailureMessage = "An error occurred while adding the book to your favorites. Please try again in a few minutes.";
            public const string RemoveFromFavoritesFailureMessage = "An error occurred while removing the book from your favorites. Please try again in a few minutes.";

            public const string AddToFavoritesSuccessMessage = "Book was added to your favorites.";
            public const string RemoveFromFavoritesSuccessMessage = "Book was removed from your favorites.";
        }

        public static class Author
        {
            public const string AuthorAlreadyExistsMessage = "Author with name '{0}' already exists.";
            public const string AuthorNotFoundMessage = "Author with id '{0}' was not found.";
            public const string AddAuthorFailureMessage = "An error occurred while adding the author. Please try again.";
            public const string DeleteAuthorFailureMessage = "An error occurred while deleting the author. Please try again.";

            public const string AddAuthorSuccessMessage = "Author was added successfully.";
            public const string DeleteAuthorSuccessMessage = "Author was deleted successfully.";
        }
        public static class Review
        {
            public const string CommentRequired = "Please write a comment.";
            public const string RatingRequired = "Please select a rating.";
            public const string AddReviewFailureMessage = "An error occurred while adding the review. Please try again.";
            public const string UnexpectedErrorMessage = "Unexpected error while adding a review.";

            public const string AddReviewSuccessMessage = "Review was added successfully.";
        }

        public static class Order
        {
            public const string BookNotFoundMessage = "The book you are trying to order was not found.";
            public const string CreateOrderFailureMessage = "An error occurred while placing your order. Please try again.";

            public const string CreateOrderSuccessMessage = "Your order was placed successfully.";
            public const string UpdateOrderStatusSuccessMessage = "Order status was updated successfully.";
        }
    }
}
