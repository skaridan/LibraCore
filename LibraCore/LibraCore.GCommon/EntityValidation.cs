namespace LibraCore.GCommon
{
    public static class EntityValidation
    {
        public static class Book
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 200;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 2000;

            public const string DefaultImageUrl = "";

            public const string BookPriceFormat = "decimal(10, 2)";
        }

        public static class Author
        {
            public const int AuthorNameMinLength = 2;
            public const int AuthorNameMaxLength = 150;
        }

        public static class Genre
        {
            public const int GenreNameMinLength = 3;
            public const int GenreNameMaxLength = 80;
        }

        public static class Rating
        {
            public const int MinRating = 1;

            public const int MaxRating = 5;

            public const int CommentMinLength = 5;
            public const int CommentMaxLength = 1000;
        }

        public static class Order
        {
            public const string OrderPriceFormat = "decimal(18, 2)";
        }

        public static class OrderItem
        {
            public const int MinOrderQuantity = 1;
            public const int MaxOrderQuantity = 10;

            public const string OrderItemPriceFormat = "decimal(18, 2)";
        }
    }
}
