import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const wait = () => new Promise((resolve) => setTimeout(resolve, 1000));

export const formatOrderReview = (n: number): string => {
  switch (n) {
    case 1:
      return "Primeira revisão deste conteúdo"
    case 2:
      return "Segunda revisão deste conteúdo"
    case 3:
      return "Última revisão deste conteúdo"
    default:
      return ""
  }
}

export const filterContentsOnComming = (contents: ContentReview[]) => {
  const today = new Date();
  return contents.filter(c => new Date(c.currentReview.scheduleReviewDate).setHours(0,0,0,0) == today.setHours(0,0,0,0))
}

export const filterContentsOverdue = (contents: ContentReview[]) => {
  const today = new Date();
  return contents.filter(c => new Date(c.currentReview.scheduleReviewDate).setHours(0,0,0,0) < today.setHours(0,0,0,0))
}
