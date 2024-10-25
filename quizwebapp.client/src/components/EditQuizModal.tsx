import React, { useState, useEffect } from "react";
import { IoMdClose } from "react-icons/io";
import { FiEdit } from "react-icons/fi";
import { motion, AnimatePresence } from "framer-motion";

const EditQuizModal = ({
  isOpen,
  onClose,
  quizId,
  initialTitle = "",
  initialTimeLimit = "",
  onSubmit,
  submitButtonText = "Save Changes",
}: {
  isOpen: boolean;
  onClose: () => void;
  quizId: number;
  initialTitle?: string;
  initialTimeLimit?: string;
  onSubmit: (quiz: { id: string, title: string; timeLimit: string }) => void;
  submitButtonText?: string;
}) => {
  const [title, setTitle] = useState(initialTitle);
  const [timeLimit, setTimeLimit] = useState(initialTimeLimit);
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  useEffect(() => {
    const handleEscape = (e: KeyboardEvent) => {
      if (e.key === "Escape") {
        onClose();
      }
    };
    window.addEventListener("keydown", handleEscape);
    return () => window.removeEventListener("keydown", handleEscape);
  }, [onClose]);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!title.trim()) {
      setError("Title cannot be empty");
      return;
    }
    if (
      isNaN(Number(timeLimit)) ||
      Number(timeLimit) < 1 ||
      Number(timeLimit) > 180
    ) {
      setError("Time limit must be between 1 and 180 minutes");
      return;
    }
    setError("");
    setSuccess("Quiz updated successfully!");
    onSubmit({ id: quizId.toString(), title, timeLimit });
    setTimeout(() => {
      setSuccess("");
      onClose();
    }, 3000);
  };

  const modalVariants = {
    hidden: { opacity: 0, scale: 0.8 },
    visible: { opacity: 1, scale: 1, transition: { duration: 0.3 } },
    exit: { opacity: 0, scale: 0.8, transition: { duration: 0.3 } },
  };

  return (
    <AnimatePresence>
      {isOpen && (
        <motion.div
          initial="hidden"
          animate="visible"
          exit="exit"
          variants={modalVariants}
          className="fixed inset-0 z-50 overflow-y-auto"
          aria-labelledby="modal-title"
          role="dialog"
          aria-modal="true"
        >
          <div className="flex items-end justify-center min-h-screen pt-4 px-4 pb-20 text-center sm:block sm:p-0">
            <div
              className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
              aria-hidden="true"
              onClick={onClose}
            ></div>

            <span
              className="hidden sm:inline-block sm:align-middle sm:h-screen"
              aria-hidden="true"
            >
              &#8203;
            </span>

            <div className="inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full">
              <div className="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
                <div className="sm:flex sm:items-start">
                  <div className="mt-3 text-center sm:mt-0 sm:ml-4 sm:text-left w-full">
                    <h3
                      className="text-lg leading-6 font-medium text-gray-900"
                      id="modal-title"
                    >
                      Edit Quiz
                    </h3>
                    <div className="mt-2">
                      <form onSubmit={handleSubmit}>
                        <div className="mb-4">
                          <label
                            htmlFor="quiz-title"
                            className="block text-sm font-medium text-gray-700"
                          >
                            Quiz Title
                          </label>
                          <input
                            type="text"
                            id="quiz-title"
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
                            placeholder="Enter quiz title"
                            required
                          />
                        </div>
                        <div className="mb-4">
                          <label
                            htmlFor="time-limit"
                            className="block text-sm font-medium text-gray-700"
                          >
                            Time Limit (minutes)
                          </label>
                          <input
                            type="number"
                            id="time-limit"
                            value={timeLimit}
                            onChange={(e) => setTimeLimit(e.target.value)}
                            className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-300 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
                            placeholder="Enter time limit"
                            min="1"
                            max="180"
                            required
                          />
                        </div>
                        {error && (
                          <p className="text-red-500 text-sm mt-2">{error}</p>
                        )}
                        {success && (
                          <p className="text-green-500 text-sm mt-2">
                            {success}
                          </p>
                        )}
                        <div className="mt-5 sm:mt-4 sm:flex sm:flex-row-reverse">
                          <button
                            type="submit"
                            className="w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-blue-600 text-base font-medium text-white hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:ml-3 sm:w-auto sm:text-sm"
                          >
                            {submitButtonText}
                          </button>
                          <button
                            type="button"
                            className="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 sm:mt-0 sm:w-auto sm:text-sm"
                            onClick={onClose}
                          >
                            Cancel
                          </button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              </div>
              <button
                onClick={onClose}
                className="absolute top-0 right-0 m-4 text-gray-500 hover:text-gray-700 focus:outline-none"
                aria-label="Close modal"
              >
                <IoMdClose size={24} />
              </button>
            </div>
          </div>
        </motion.div>
      )}
    </AnimatePresence>
  );
};

export default EditQuizModal;
