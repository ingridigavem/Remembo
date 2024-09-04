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
