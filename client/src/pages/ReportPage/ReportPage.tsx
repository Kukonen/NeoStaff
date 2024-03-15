import { useState } from "react";

import './ReportPage.css'
import Report from "../../components/Report/Report";
import ReportGraphics from "../../components/ReportGraphics/ReportGraphics";

const ReportPage = () => {
    const [serviceNumber, setServiceNumber] = useState<string>("");

    return (
      <div id="report__page">
            <div id="report">
                <div id="report__service-number">
                    <span>
                        Табельный номер:
                    </span>
                    <input 
                        type="text" 
                        value={serviceNumber}
                        onChange={e => setServiceNumber(e.target.value)}
                    />
                </div>

                <Report serviceNumber={serviceNumber}/>

                <ReportGraphics serviceNumber={serviceNumber}/>
            </div>
        </div>
    );
};
  
  export default ReportPage;