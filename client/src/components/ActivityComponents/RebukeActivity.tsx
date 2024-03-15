import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const RebukeActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            reason: string
        }
    >(
        {
            reason: "",
        }
    );
    const [reason, setReason] = useState<string>("")

    const changeReason = (newReason: string) => {
        setReason(newReason);

        let newData = currentData;
        currentData.reason = newReason;

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <>
            <tr>
                <td>Причина: </td>
                <td>
                    <input 
                        value={reason}
                        onChange={e => changeReason(e.target.value)}
                    />
                </td>
            </tr>
        </>
    )
}

export default RebukeActivity;