// import { serverAdress } from '../settings.json';

export type queryType = {
    key: string,
    value: string | number
};

// const serverAdress = "http://localhost:8080/api/staff/";
const serverAdress = "http://localhost:3030/";

type methodType = 'GET' | 'POST' | 'PUT' | 'DELETE'

const urlFormatter = (URL: string, queries?: queryType[]): string => {
    let formatterURL: string = serverAdress;
    formatterURL += URL[0] === '/' ? URL.substring(1) : URL;

    if (queries) {
        const params = new URLSearchParams();

        for (let q of queries) {
            params.append(q.key, String(q.value));
        }

        formatterURL += '?' + params.toString();
    }

    return formatterURL;
}

const fetchResponseStatus = (response: Response, params: {
    URL: string,
    method: methodType,
    queryParams?: queryType[],
    body?: Object | string
}) => {
    if (!response.ok) {
        return errorResolve(response, params);
    }

    return Promise.resolve(response);
}

const fetchJson = (response: Response) => {
    return Promise.resolve(response.json());
}

const errorResolve = (error: Response, params: {
    URL: string,
    method: methodType,
    queryParams?: queryType[],
    body?: Object | string
}) : Promise<any> => {

    return Promise.reject(error)
}


const server = async (
    URL: string,
    method: methodType = 'GET',
    queryParams?: queryType[],
    body?: Object | string
) => {
    let formatterURL:string = urlFormatter(URL, queryParams);

    let init:RequestInit = {};

    init.method = method;

     init.headers = {
        'Content-Type': 'application/json;charset=utf-8',
    };

    let bodyFormatter: string = '';

    if(body) {
        if (typeof body === 'string') {
            bodyFormatter = body;
        } else {
            bodyFormatter = JSON.stringify(body);
        }
        init.body = bodyFormatter;
    }

    return fetch(formatterURL, init)
        .then(response => fetchResponseStatus(response, {
            URL,
            method,
            queryParams,
            body
        }))
        .then(fetchJson)
        .catch((error: Response) => {
            return Promise.reject(error);
        });

}

export default server;