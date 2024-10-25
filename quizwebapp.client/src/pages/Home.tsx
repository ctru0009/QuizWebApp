import { toast } from "react-toastify";
import { Quiz } from "../Schemas";
import { useEffect, useState } from "react";
import { FiEdit, FiBook } from "react-icons/fi";
import EditQuizModal from "../components/EditQuizModal";
import { useNavigate } from "react-router-dom";

function Home() {
  const [quizzes, setQuizzes] = useState<Quiz[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const BACKEND_URL = "https://localhost:7210";

  const navigate = useNavigate();

  useEffect(() => {
    fetchQuizzes();
    console.log(quizzes);
  }, []);

  const fetchQuizzes = async () => {
    const response = await fetch(`${BACKEND_URL}/api/quizzes`);
    if (response.ok) {
      const data = await response.json();
      setQuizzes(data);
      console.log(quizzes);
    } else {
      toast.error("Failed to fetch quizzes");
    }
  };

  const handleEdit = (data: { id: string; title: string; timeLimit: string }) => {
    fetch(`${BACKEND_URL}/api/quizzes/${data.id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        quizTitle: data.title,
        timeLimit: data.timeLimit,
      }),
    })
      .then((response) => {
        if (response.ok) {
          toast.success("Quiz updated successfully!");
          fetchQuizzes();
        } else {
          toast.error("Failed to update quiz: " + response.statusText);
        }
      })
      .catch(() => {
        toast.error("Failed to update quiz");
      });
  };

  return (
    <div className="container mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <div className="overflow-x-auto shadow-md sm:rounded-lg">
        <table className="w-full text-sm text-left text-gray-500">
          <thead className="text-xs text-gray-700 uppercase bg-gray-50">
            <tr>
              <th scope="col" className="px-6 py-3" aria-label="Quiz ID">
                Quiz ID
              </th>
              <th scope="col" className="px-6 py-3" aria-label="Quiz Title">
                Quiz Title
              </th>
              <th scope="col" className="px-6 py-3" aria-label="Time Limit">
                Time Limit
              </th>
              <th scope="col" className="px-6 py-3" aria-label="Created At">
                Created At
              </th>
              <th scope="col" className="px-6 py-3" aria-label="Updated At">
                Updated At
              </th>
              <th scope="col" className="px-6 py-3" aria-label="Actions">
                Actions
              </th>
            </tr>
          </thead>
          <tbody>
            {quizzes.map((quiz) => (
              <tr
                key={quiz.quizId}
                className="bg-white border-b hover:bg-gray-50 transition-colors duration-200"
              >
                <td className="px-6 py-4">{quiz.quizId}</td>
                <td className="px-6 py-4 font-medium text-gray-900 whitespace-nowrap">
                  {quiz.quizTitle}
                </td>
                <td className="px-6 py-4">{quiz.timeLimit} second(s)</td>
                <td className="px-6 py-4">
                  {new Date(quiz.createdAt).toLocaleString()}
                </td>
                <td className="px-6 py-4">
                  {new Date(quiz.updatedAt).toLocaleString()}
                </td>
                <td className="px-6 py-4 flex space-x-2">
                  <button
                    aria-label="Edit quiz"
                    className="text-blue-600 hover:text-blue-900 transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 rounded-full p-1"
                    onClick={() => setIsModalOpen(true)}
                  >
                    <FiEdit className="w-5 h-5" />
                  </button>
                  <EditQuizModal
                    isOpen={isModalOpen}
                    onClose={() => setIsModalOpen(false)}
                    quizId={quiz.quizId}
                    initialTitle={quiz.quizTitle}
                    initialTimeLimit={quiz.timeLimit.toString()}
                    onSubmit= {handleEdit}
                    submitButtonText="Update Quiz"
                    />
                  <button
                    aria-label="Take quiz"
                    className="text-green-500 hover:text-green-800 transition-colors duration-200 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-opacity-50 rounded-full p-1"
                    onClick={() => {
                      // Check if quiz has questions
                        navigate(`/quiz/attemp/${quiz.quizId}`);
                    }}
                  >
                    <FiBook className="w-5 h-5" />
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Home;
