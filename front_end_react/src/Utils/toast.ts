
import { toast } from 'react-toastify';

export const showSuccess = (message: string) => {
  toast.success(message);
};

export const showError = (error: any) => {
  const message =
    typeof error === "string"
      ? error
      : error?.message || "Something went wrong";
  toast.error(message);
};

export const showInfo = (message: string) => {
  toast.info(message);
};

export const showWarning = (message: string) => {
  toast.warn(message);
};
