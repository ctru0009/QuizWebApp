import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { Question } from "../Schemas";
const CreateQuestion = () => {
  const [quizId, setQuizId] = useState(0);
  const [questionText, setQuestionText] = useState("");
  const [answer1, setAnswer1] = useState("");
  const [answer2, setAnswer2] = useState("");
  const [answer3, setAnswer3] = useState("");
  const [answer4, setAnswer4] = useState("");
  const [correctAnswer, setCorrectAnswer] = useState(1);
  const [questions, setQuestions] = useState([]);
  const BACKEND_URL = "https://localhost:7210";

  useEffect(() => {
    fetchQuestions();
  }, []);

  const fetchQuestions = async () => {
    const response = await fetch(`${BACKEND_URL}/api/questions`);
    if (response.ok) {
      const data = await response.json();
      setQuestions(data);
    } else {
      toast.error("Failed to fetch questions");
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const response = await fetch(`${BACKEND_URL}/api/questions`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        quizId: quizId,
        questionText,
        createAnswersDto: [
          { answerText: answer1, isCorrect: correctAnswer === 1 },
          { answerText: answer2, isCorrect: correctAnswer === 2 },
          { answerText: answer3, isCorrect: correctAnswer === 3 },
          { answerText: answer4, isCorrect: correctAnswer === 4 },
        ],
      }),
    });
    if (response.ok) {
      toast.success("Question created successfully!");
      setQuizId(0);
      setQuestionText("");
      setAnswer1("");
      setAnswer2("");
      setAnswer3("");
      setAnswer4("");
      setCorrectAnswer(1);
    } else {
      toast.error("Failed to create question");
    }
  };

  return (
    <>
      <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <form
          onSubmit={handleSubmit}
          className="p-4 bg-white rounded-lg shadow-md"
        >
          <h2 className="text-xl font-semibold text-center">Create Question</h2>
          <div className="flex flex-col space-y-4">
            <label htmlFor="quizId" className="text-sm">
              Quiz ID
            </label>
            <input
              type="number"
              id="quizId"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setQuizId(parseInt(e.target.value))}
              required
            />
            <label htmlFor="question" className="text-sm">
              Question Text
            </label>
            <input
              type="text"
              id="question"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setQuestionText(e.target.value)}
              required
            />
            <label htmlFor="option1" className="text-sm">
              Option 1
            </label>
            <input
              type="text"
              id="option1"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setAnswer1(e.target.value)}
              required
            />
            <label htmlFor="option2" className="text-sm">
              Option 2
            </label>
            <input
              type="text"
              id="option2"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setAnswer2(e.target.value)}
              required
            />
            <label htmlFor="option3" className="text-sm">
              Option 3
            </label>
            <input
              type="text"
              id="option3"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setAnswer3(e.target.value)}
              required
            />
            <label htmlFor="option4" className="text-sm">
              Option 4
            </label>
            <input
              type="text"
              id="option4"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setAnswer4(e.target.value)}
              required
            />
            <label htmlFor="correctOption" className="text-sm">
              Correct Option
            </label>
            <input
              type="number"
              id="correctOption"
              className="p-2 border border-gray-300 rounded-md"
              onChange={(e) => setCorrectAnswer(parseInt(e.target.value))}
              min="1"
              max="4"
              required
            />
            <button
              type="submit"
              className="bg-blue-500 text-white rounded-md p-2"
            >
              Create Question
            </button>
          </div>
        </form>

        <div className="mt-8">
          <h2 className="text-xl font-semibold text-center">Questions</h2>
          <div className="flex flex-col space-y-4">
            {questions.map((question: Question) => (
              <div
                key={question.questionId}
                className="p-4 bg-white rounded-lg shadow-md"
              >
                <h3 className="text-lg font-semibold">
                  {question.questionText}
                </h3>
                <ul className="list-disc list-inside">
                  {question.answers.map((answer) => (
                    <li
                      key={answer.answerId}
                      className={
                        answer.isCorrect ? "text-green-500" : "text-red-500"
                      }
                    >
                      {answer.answerText}
                    </li>
                  ))}
                </ul>
              </div>
            ))}
          </div>
        </div>
      </div>
    </>
  );
};

export default CreateQuestion;
