import { HiAcademicCap } from "react-icons/hi";

const NavBar = () => {
  return (
    <div>
      <nav className="bg-blue-500 p-4">
        <div className="container mx-auto">
          <div className="flex justify-between items-center">
            <div className="flex">
              <HiAcademicCap className="space-x-3 text-3xl" />
              <a href="/" className="text-white text-2xl font-bold">
                Quiz Master
              </a>
            </div>
            <div>
              <a
                href="/create-quiz"
                className=" text-white ml-4 bg-blue-600 p-2 rounded-lg hover:bg-blue-700"
              >
                Create Quiz
              </a>
              <a
                href="/create-question"
                className="text-white ml-4 p-2 bg-blue-600 rounded-lg hover:bg-blue-700"
              >
                Create Question
              </a>
              <a
                href="/results"
                className="text-white ml-4 p-2 bg-blue-600 rounded-lg hover:bg-blue-700"
              >
                Results
              </a>
            </div>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default NavBar;
