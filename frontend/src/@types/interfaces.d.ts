interface User {
    email: string,
    name: string,
    userId: string,
}

interface Matter {
    id: string,
    name: string,
}

interface Review {
    id: string,
    content: string,
    isReviewed: boolean,
    reviewNumber: number,
    scheduleReviewDate: Date,
    note: string,
}

interface DetailedNewContent {
    matter: MatterWithNewContent;
}

interface MatterWithNewContent {
    matterId:      string;
    matterName:    string;
    contentReview: ContentReview;
}

interface Dashboard {
    statistics: Statistics
    matterDetailsList: DetailedContent[]
}

interface Statistics {
    completedReviewsTotal: number
    completedContentTotal: number
    notCompletedContentTotal: number
}

interface DetailedContent {
    matter: MatterWithContent;
}

interface MatterWithContent {
    matterId:      string;
    matterName:    string;
    contents: ContentReview[];
}

interface ContentReview {
    contentId:     string;
    contentName:   string;
    note:          string;
    reviewNumber:  number;
    currentReview: CurrentReview;
}

interface CurrentReview {
    reviewId:           string;
    scheduleReviewDate: string;
    isReviewed:         boolean;
}
