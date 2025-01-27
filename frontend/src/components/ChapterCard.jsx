import React from "react";
import Card2 from "./Card2";
import Play from '../../public/images/Play.png';
import Dots from '../../public/images/Dots.png';
import "../styles/CardHolder.css";

export default function ChapterCard() {
/*     const [courses, setCourses] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7002/api/courses")
      .then(res => res.json())
      .then(data => setCourses(data));
  }, []);


 */
  return (
    <div className="flex flex-col md:flex-rox overflow-x-auto h-auto justify-between sm:px-5 md:px-10 lg:px-14 xl:px-20">
      <div className="h-auto flex items-center flex-row justify-between border-black border-2 custom-shadow"> 
          
        <p className="font-bold text-xl sm:text-3xl m-5"> CHAPTERS </p>
        <div className="flex flex-row justify-between items-center">
          <img src={Dots} className='w-8 h-8 sm:w-14 sm:h-14 mx-2 '></img> 
          <p className="text-3xl mr-2"> 15 </p> {/* /// */}
          <img src={Play} className='rounded-full w-10 h-10 sm:w-14 sm:h-14 sm:ml-14 mr-5 '></img> 
        </div>
      </div>
      
      <div className="flex flex-row border-black overflow-y-hidden custom-scrollbar border">
        {/* {courses.map((data) => {
        return <Card2 key={data.coursesChapterID} title={data.coursesChapterName} description={data.coursesChapterDescription}/>
      })} */}
        <div className="border-zinc-700 border-r h-auto flex flex-none flex-col items-start w-full sm:w-1/3 mt-3 pl-3 overflow-hidden">
            <p className="mt-2 sm:mt-4 font-bold text-xl">Chapter Name</p>
            <p className="mt-2 sm:mt-4 font-medium">Chapter description Lorem ipsum, dolor sit amet consectetur adipisicing elit. Aliquid rem quisquam architecto labore in voluptatem quaerat exercitationem officia sit! Inventore iure rem minima quam fugiat dolorem quisquam, assumenda aperiam beatae?</p>
           
            <div className="font-medium pt-5 pb-3 self-center">
                <ul className="list-disc">
                    <li><p>Material 1 <a href="/">Material 1</a> </p></li>
                    <li><p>Material 2 <a href="/">Material 2</a> </p></li>
                    <li><p>Material 3 <a href="/">Material 3</a> </p></li>
                </ul>
            </div>      
        </div>  
        <div className="border-zinc-700 border-r h-auto flex flex-none flex-col items-start w-full sm:w-1/3 mt-3 pl-3 overflow-hidden">
            <p className="mt-2 sm:mt-4 font-bold text-xl">Chapter Name</p>
            <p className="mt-2 sm:mt-4 font-medium">Chapter description Lorem ipsum, dolor sit amet consectetur adipisicing elit. Aliquid rem quisquam architecto labore in voluptatem quaerat exercitationem officia sit! Inventore iure rem minima quam fugiat dolorem quisquam, assumenda aperiam beatae?</p>
           
            <div className="font-medium pt-5 pb-3 self-center">
                <ul className="list-disc">
                    <li><p>Material 1 <a href="/">Material 1</a> </p></li>
                    <li><p>Material 2 <a href="/">Material 2</a> </p></li>
                    <li><p>Material 3 <a href="/">Material 3</a> </p></li>
                </ul>
            </div>      
        </div>    
        <div className="border-zinc-700 border-r h-auto flex flex-none flex-col items-start w-full sm:w-1/3 mt-3 pl-3 overflow-hidden">
            <p className="mt-2 sm:mt-4 font-bold text-xl">Chapter Name</p>
            <p className="mt-2 sm:mt-4 font-medium">Chapter description Lorem ipsum, dolor sit amet consectetur adipisicing elit. Aliquid rem quisquam architecto labore in voluptatem quaerat exercitationem officia sit! Inventore iure rem minima quam fugiat dolorem quisquam, assumenda aperiam beatae?</p>
           
            <div className="font-medium pt-5 pb-3 self-center">
                <ul className="list-disc">
                    <li><p>Material 1 <a href="/">Material 1</a> </p></li>
                    <li><p>Material 2 <a href="/">Material 2</a> </p></li>
                    <li><p>Material 3 <a href="/">Material 3</a> </p></li>
                </ul>
            </div>      
        </div>            
      </div>
    </div>
  );
}