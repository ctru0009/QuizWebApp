import React, { useState } from "react";
import { toast } from "react-toastify";
import { Quiz } from "../Schemas";
const CreateQuiz = () => {
  const BACKEND_URL = "https://localhost:7210";
  const [quizTitle, setQuizTitle] = useState("");
  const [timeLimit, setTimeLimit] = useState(0);
  const [userId, setUserId] = useState("");

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const response = await fetch(`${BACKEND_URL}/api/quizzes`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        userId,
        quizTitle,
        timeLimit,
      }),
    });
    if (response.ok) {
      toast.success("Quiz created successfully!");
      setQuizTitle("");
      setTimeLimit(0);
      setUserId("");
    } else {
      toast.error("Failed to create quiz");
    }
  };
  return (
    <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <form
        onSubmit={handleSubmit}
        className="p-4 bg-white rounded-lg shadow-md"
      >
        <h2 className="text-xl font-semibold text-center">Create Quiz</h2>
        <div className="flex flex-col space-y-4">
        <label htmlFor="userId" className="text-sm">
            User ID
          </label>
          <input
            type="text"
            id="userId"
            className="p-2 border border-gray-300 rounded-md"
            onChange={(e) => setUserId(e.target.value)}
            required
          />
          <label htmlFor="quiz-title" className="text-sm">
            Quiz Title
          </label>
          <input
            type="text"
            id="quiz-title"
            className="p-2 border border-gray-300 rounded-md"
            onChange={(e) => setQuizTitle(e.target.value)}
            required
          />
          <label htmlFor="time-limit" className="text-sm">
            Time Limit (minutes)
          </label>
          <input
            type="number"
            id="time-limit"
            className="p-2 border border-gray-300 rounded-md"
            onChange={(e) => setTimeLimit(parseInt(e.target.value))}
            required
          />
          <button
            type="submit"
            className="bg-blue-500 text-white rounded-md p-2"
          >
            Create Quiz
          </button>
        </div>
      </form>
    </div>
  );
};

export default CreateQuiz;
