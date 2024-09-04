interface User {
  email: string;
  name: string;
  userId: string;
}

interface Subject {
  id: string;
  name: string;
}

interface Review {
  id: string;
  contentId: string;
  isReviewed: boolean;
  scheduleReviewDate: string;
}

interface DetailedNewContent {
  subject: SubjectWithNewContent;
}

interface SubjectWithNewContent {
  subjectId: string;
  subjectName: string;
  contentReview: ContentReview;
}

interface Dashboard {
  statistics: Statistics;
  subjectDetailsList: DetailedContent[];
}

interface Statistics {
  completedReviewsTotal: number;
  completedContentTotal: number;
  notCompletedContentTotal: number;
}

interface DetailedContent {
  subject: SubjectWithContent;
}

interface SubjectWithContent {
  subjectId: string;
  subjectName: string;
  contents: ContentReview[];
}

interface ContentReview {
  contentId: string;
  contentName: string;
  note: string;
  reviewNumber: number;
  currentReview: CurrentReview;
}

interface Content {
  id: string;
  name: string;
  note: string;
  reviewNumber: number;
}

interface CurrentReview {
  reviewId: string;
  scheduleReviewDate: string;
  isReviewed: boolean;
}
