import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const wait = () => new Promise((resolve) => setTimeout(resolve, 1000));

export const formatOrderReview = (n: number) => {
  switch (n) {
    case 1:
      return "Primeira revisão deste conteúdo"
    case 2:
      return "Segunda revisão deste conteúdo"
    case 3:
      return "Última revisão deste conteúdo"
    default:
      return
  }
}
