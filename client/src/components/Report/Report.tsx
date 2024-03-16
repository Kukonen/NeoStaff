import { useEffect, useState } from "react";
import ReportActivity, { TypeActivitiesFields } from "../ReportActivity/ReportActivity";
import ReportService from "../../service/ReportService";

import './Report.css'

interface ReportProps {
    serviceNumber: string
}

const Report = ({serviceNumber}: ReportProps) => {
    const [activities, setActivities] = useState<TypeActivitiesFields>([])

    useEffect(() => {
        if (serviceNumber === "") {
            return;
        }

        ReportService.getActivities(serviceNumber).then(activitiesFromServer => {
            // @ts-ignore
            setActivities(activitiesFromServer.map( act => [["type", act.type], ...Object.entries(act.activityInfo)]) as TypeActivitiesFields)
        }).catch(() => {
            setActivities([])
        })
    }, [])

    useEffect(() => {
        if (serviceNumber === "") {
            return;
        }

        ReportService.getActivities(serviceNumber).then((activitiesFromServer) => {
            // @ts-ignore
            // console.log(activitiesFromServer.map( act => [["type", act.type], ...Object.entries(act.activityInfo)]))
            // @ts-ignore
            setActivities(activitiesFromServer.map( act => [["type", act.type], ...Object.entries(act.activityInfo)]) as TypeActivitiesFields)
        }).catch(() => {
            setActivities([])
        })
    }, [serviceNumber]);

    return (
            <div id="report__tables">
         {/* <table className="table__action">
             <tbody> */}
                {activities.map(activity => <ReportActivity typeActivitiesFields={activity as TypeActivitiesFields} />)}
            {/* </tbody>
        </table> */}
            </div>
    )
}

export default Report;