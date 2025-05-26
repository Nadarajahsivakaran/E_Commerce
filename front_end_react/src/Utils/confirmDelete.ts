
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";

const MySwal = withReactContent(Swal);

export const confirmDelete = async (
  title = "Are you sure?",
  text = "You won’t be able to undo this!",
  confirmButtonText = "Yes, delete it!"
): Promise<boolean> => {
  const result = await MySwal.fire({
    title,
    text,
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#d33",
    cancelButtonColor: "#3085d6",
    confirmButtonText,
    cancelButtonText: "Cancel",
  });

  return result.isConfirmed;
};
