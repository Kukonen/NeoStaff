import { useEffect, useMemo, useState } from "react";
import { converTypeToFormatType } from "../../helpers/ActivityHelper";
import { dateConverter, titlesConverter } from "../../helpers/ActivityHelper";

export type TypeActivitiesFields = [
    string[]
] | [];

interface ReportActivityProps {
    typeActivitiesFields: any
}

const ReportActivity = ({typeActivitiesFields} : ReportActivityProps) => {

    // const formatterActivities = typeActivitiesFields.filter(field => field[0] !== 'type')

    // const typeActivities = typeActivitiesFields.find(field => field[0] == 'type')

    const [activities, setActivities] = useState<TypeActivitiesFields>([])
    const [header, setHeader] = useState<string>("")

    useEffect(() => {
            let tempHeader = "";
            let tempActivities:any[][] = [];
            console.log(typeActivitiesFields)
            for (const field in typeActivitiesFields) {
                console.log(field, typeActivitiesFields[field])
                if (field === "_id") {
                    continue;
                }

                if (field === "type") {
                    if(tempHeader === "") {
                        tempHeader += titlesConverter(typeActivitiesFields[field]) + " | ";
                    } else {
                        tempHeader = titlesConverter(typeActivitiesFields[field]) + " | " + tempHeader;
                    }

                    continue;
                }

                if (field === "date") {
                    tempHeader += dateConverter(typeActivitiesFields[field]);

                    continue;
                }

                if (field === "activityInfo") {
                    Object.entries(typeActivitiesFields[field]).forEach((element:any[]) => {
                        tempActivities.push([titlesConverter(element[0]) , titlesConverter(element[1])]);
                    });

                    continue;
                }

                tempActivities.push([titlesConverter(field), titlesConverter(typeActivitiesFields[field])]);
            }
            setHeader(tempHeader)
            // @ts-ignore
            setActivities(tempActivities);
    }, [])

    return (
    <table className="table__action">
        <thead>
            <tr>
                <td colSpan={2} className="table__action__header">
                    {header}
                    <hr />
                </td>
            </tr>
        </thead>
        <tbody className="table__action__block">
                {
                    activities.map(field => {
                        return (
                            <tr>
                                <td className={field[1].length > 48 ? "td_top-diection" : undefined}>
                                    {field[0]}
                                </td>
                                <td>
                                    {
                                        field[1].length > 48 ?
                                        <textarea rows={3} disabled>
                                            {field[1]}
                                        </textarea>
                                        :
                                         <input 
                                            type="text" 
                                            value={field[1]}
                                            disabled
                                        />
                                    }
                                    
                                </td>
                            </tr>
                        )
                    })
                }
        </tbody>
    </table>
    )
}

export default ReportActivity;