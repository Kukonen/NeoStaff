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
                <td className="td_top-diection">Причина: </td>
                <td>
                    <textarea 
                        value={reason}
                        onChange={e => changeReason(e.target.value)}
                        rows={3}
                    />
                </td>
            </tr>
        </>
    )
}

export default RebukeActivity;