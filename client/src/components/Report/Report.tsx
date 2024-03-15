import { useEffect, useState } from "react";
import ReportActivity, { TypeActivitiesFields } from "../ReportActivity/ReportActivity";
import ReportService from "../../service/ReportService";

interface ReportProps {
    serviceNumber: string
}

const Report = ({serviceNumber}: ReportProps) => {
    const [activities, setActivities] = useState<TypeActivitiesFields>([])

    useEffect(() => {
        ReportService.getActivities(serviceNumber).then(activitiesFromServer => {
            setActivities(activitiesFromServer as TypeActivitiesFields)
        })
    }, [])

    return (
        <div>
            {
                activities.map(activity => <ReportActivity typeActivitiesFields={activity as TypeActivitiesFields} />)
            }
        </div>
    )
}

export default Report;