import { useEffect, useState } from "react";
import ActivityComponentsProps from "./ActivityComponentsProps";

const SkillsActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            skill: string, 
            report: string,
        }
    >(
        {
            skill: "", 
            report: "",
        }
    );
    const [skill, setSkill] = useState<string>("")
    const [report, setReport] = useState<string>("")

    const changeSkill = (newSkill: string) => {
        setSkill(newSkill);

        let newData = currentData;
        currentData.skill = newSkill;

        setCurrentData(newData);

        setData(newData);
    }
    
    const changeReport = (newReport: string) => {
        setReport(newReport);

        let newData = currentData;
        currentData.report = newReport;

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <>
            <tr>
                <td>Навык: </td>
                <td>
                    <input 
                        type="text"
                        value={skill}
                        onChange={e => changeSkill(e.target.value)}
                    />
                </td>
            </tr>

            <tr>
                <td>Отзыв от организатора</td>
                <td>
                    <textarea 
                        value={report}
                        onChange={e => changeReport(e.target.value)}
                        rows={3}
                    />
                </td>
            </tr>
        </>
    )
}

export default SkillsActivity;