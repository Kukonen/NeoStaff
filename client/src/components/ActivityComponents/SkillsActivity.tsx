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
                <td>Место соревнования: </td>
                <td>
                    <input 
                        value={skill}
                        onChange={e => changeSkill(e.target.value)}
                    />
                </td>
            </tr>

            <tr>
                <td>Тема соревнования: </td>
                <td>
                    <input 
                        value={report}
                        onChange={e => changeReport(e.target.value)}
                    />
                </td>
            </tr>
        </>
    )
}

export default SkillsActivity;