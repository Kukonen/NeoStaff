import {RouteObject, createBrowserRouter} from "react-router-dom";

import Layout from "../layout/Layout";
import Error from "../layout/Error";
import HomePage from "../pages/HomePage/HomePage";
import ActivityPage from "../pages/ActivityPage/ActivityPage";
import ReportPage from "../pages/ReportPage/ReportPage";

const routes: RouteObject[] = [
    {
        path: "/",
        element: <Layout />,
        children: [
            {
                path: '/',
                element: <HomePage />
            },
            {
                path: 'activity',
                element: <ActivityPage />
            },
            {
                path: 'report',
                element: <ReportPage />
            },
            {
                path: '*',
                element: <Error errorMessage={"Такой страницы не существует"} />,
                errorElement: <Error errorMessage={"Что-то пошло не так"} />
            }
        ]
    }
    
];

export const routing = createBrowserRouter(routes);