namespace Remembo.Domain.Shared.Constants;

public static class ErrorsMessages {
    public const string NULL_REQUEST_ERROR = "Request can not be null";
    public const string NULL_USER_ID_ERROR = "User Id can not be null";
    public const string NULL_ID_ERROR = "Id can not be null";
    public const string FAILED_TO_PERSIST_DATA_ERROR = "Failed to persist data";
    public const string FAILED_TO_RETRIEVE_DATA_ERROR = "Failed to retrieve data";
    public const string FAILED_TO_CREATE_NEXT_REVIEW_ERROR = "Failed to create next review";
    public const string FAILED_TO_UPDATE_CURRENT_REVIEW_ERROR = "Failed to update current review";
    public const string USER_PASSWORD_INVALID_ERROR = "User/Password invalid";
    public const string EMAIL_ALREADY_REGISTERED_ERROR = "Email already registered";
    public const string MAXIMUM_NUMBER_REVIEWS_REACHED = "Maximum number of reviews reached";
}

public static class SuccessMessages {
    public const string NEXT_REVIEW_DATE = "Next review date: ";
    public const string ALL_REVIEWS_FINISHED = "Congrats! You finished all reviews from this content.";
}