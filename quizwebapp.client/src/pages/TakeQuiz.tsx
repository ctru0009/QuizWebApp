import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { BACKEND_URL } from "../constants";
import { TakeQuiz as TakingQuizObj } from "../Schemas";
import { FaRegClock } from "react-icons/fa";
const TakeQuiz = () => {
  const QuizId = useParams().id;
  const navigate = useNavigate();
  const [takeId, setTakeId] = useState<number | null>(0);
  const [quiz, setQuiz] = useState<TakingQuizObj | null>(null);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [userAnswers, setUserAnswers] = useState<{
    [questionId: number]: number;
  }>({});
  const [answerRequired, setAnswerRequired] = useState(false);
  const [timeRemaining, setTimeRemaining] = useState<number | null>(null);

  useEffect(() => {
    fetchQuestions();
  }, []);

  const fetchQuestions = async () => {
    const response = await fetch(`${BACKEND_URL}/api/Takes/${QuizId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "*/*",
      },
      body: JSON.stringify({ quizId: QuizId, userId: 1 }),
    });
    const data = await response.json();
    setQuiz(data);
    setTakeId(data.takeId);
    setTimeRemaining(data.timeLimit);
  };

  useEffect(() => {
    if (timeRemaining === null || timeRemaining <= 0) return;

    const timer = setInterval(() => {
      setTimeRemaining((prev) => (prev !== null ? prev - 1 : null));
    }, 1000);

    return () => clearInterval(timer);
  }, [timeRemaining]);

  useEffect(() => {
    if (answerRequired) {
      toast.error("Please select an answer to proceed");
    }
  }, [answerRequired]);

  const handleSubmit = (e: React.MouseEvent<HTMLButtonElement>) => {
    if (!quiz) return;

    const currentQuestionId = quiz.questions[currentQuestionIndex].questionId;
    if (userAnswers[currentQuestionId] === undefined) {
      setAnswerRequired(true);
      return;
    }
    const takeAnswers = Object.entries(userAnswers).map(
      ([questionId, answerId]) => ({
        questionId: parseInt(questionId),
        answerId,
      })
    );

    const takeQuizObj = {
      takeId: quiz.takeId,
      takeAnswers,
    };
    fetch(`${BACKEND_URL}/api/takes/submit/${takeId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(takeQuizObj),
    }).then((response) => {
      if (response.ok) {
        toast.success("Quiz submitted successfully!");
        navigate("/results/" + takeId);
      } else {
        toast.error("Failed to submit quiz");
      }
    });
  };

  const handleAnswerSelect = (questionId: number, answerId: number) => {
    setUserAnswers((prev) => ({ ...prev, [questionId]: answerId }));
    setAnswerRequired(false);
  };

  const handleNextQuestion = () => {
    if (quiz) {
      const currentQuestionId = quiz.questions[currentQuestionIndex].questionId;
      if (userAnswers[currentQuestionId] === undefined) {
        setAnswerRequired(true);
        return;
      }

      if (currentQuestionIndex < quiz.questions.length - 1) {
        setCurrentQuestionIndex((prev) => prev + 1);
        setAnswerRequired(false);
      }
    }
  };

  const handlePreviousQuestion = () => {
    if (currentQuestionIndex > 0) {
      setCurrentQuestionIndex((prev) => prev - 1);
    }
  };

  if (!quiz) return <div>Loading...</div>;
  const currentQuestion = quiz.questions[currentQuestionIndex];
  return (
    <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <div className="p-8 max-w-4xl mx-auto">
        <h1 className="text-2xl font-bold mb-4">{quiz.quizTitle}</h1>
        <div className="flex justify-between items-center mb-4">
          <span>
            Question {currentQuestionIndex + 1} of {quiz.totalQuestion}
          </span>
          <span className="flex items-center">
            <FaRegClock className="mr-2" />
            Time remaining: {Math.floor(timeRemaining! / 60)}:
            {(timeRemaining! % 60).toString().padStart(2, "0")}
          </span>
        </div>
        <h2 className="text-xl font-semibold mb-4">
          {currentQuestion.questionText}
        </h2>
        <div className="space-y-2">
          {currentQuestion.takeAnswerDtos.map((answer) => (
            <div key={answer.answerId} className="flex items-center">
              <input
                type="radio"
                id={`answer-${answer.answerId}`}
                name={`question-${currentQuestion.questionId}`}
                value={answer.answerId}
                checked={
                  userAnswers[currentQuestion.questionId] === answer.answerId
                }
                onChange={() =>
                  handleAnswerSelect(
                    currentQuestion.questionId,
                    answer.answerId
                  )
                }
                className="mr-2"
              />
              <label htmlFor={`answer-${answer.answerId}`}>
                {answer.answerText}
              </label>
            </div>
          ))}
        </div>
        <div className="mt-8 flex justify-between">
          <button
            onClick={handlePreviousQuestion}
            disabled={currentQuestionIndex === 0}
            className="px-4 py-2 bg-gray-200 text-gray-800 rounded disabled:opacity-50"
          >
            Previous
          </button>
          {currentQuestionIndex < quiz.questions.length - 1 ? (
            <button
              onClick={handleNextQuestion}
              className="px-4 py-2 bg-blue-500 text-white rounded"
            >
              Next
            </button>
          ) : (
            <button
              onClick={handleSubmit}
              className="px-4 py-2 bg-green-500 text-white rounded"
            >
              Submit Quiz
            </button>
          )}
        </div>
      </div>
    </div>
  );
};

export default TakeQuiz;
