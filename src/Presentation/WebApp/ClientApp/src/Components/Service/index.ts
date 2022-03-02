const setConfig = (method: string, body: any) => {
    return {
        method: method,
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': 'Allow',
        },
        body: body ? JSON.stringify(body) : null
    }
}

const setConfigFile = (method: string, body: any) => {
    return {
        method: method,
        headers: {
            'contentType': 'false',
            'Access-Control-Allow-Origin': 'Allow',
        },
        body: body
    }
}
const GetResponse = async <T>(url: string, methodType: string, body: any, isFileUploading: boolean = false) => {
    try {
        const config = !isFileUploading ? setConfig(methodType, body) : setConfigFile(methodType, body)
        const response = await fetch(url, config);
        if (response.status === 401) {
            window.location.href = '/login';
        }
        if (response.status === 404) {
            return {
                resultObject: null,
                hasErrors: true,
                errors: [`API ${url?.split('?')[0]} not found`]
            }
        }
        var result = await response.json() as T;
        return result;
    } catch (error) {
        return {
            resultObject: null,
            hasErrors: true,
            errors: error as string[]
        }
    }
}

const Get = async <T>(url: string) => {
    return await GetResponse<T>(url, "GET", null)
}

const PostImage = async <T>(url: string, body: any) => {
    return await GetResponse<T>(url, "POST", body, true)
}

const Post = async <T>(url: string, body: any) => {
    return await GetResponse<T>(url, "POST", body)
}

const Put = async <T>(url: string, body: any) => {
    return await GetResponse<T>(url, "PUT", body)
}

const Delete = async <T>(url: string, body: any) => {
    return await GetResponse<T>(url, "DELETE", body)
}

const ApplyPaging = (Skip: number, RowCount: number, Take: number) => {
    let _skip = Skip;
    let _total = RowCount;

    var nextDisabled = false;
    var prevDisabled = false;

    let take = 0;
    if (_skip + Take >= _total) {
        nextDisabled = true;
        take = _total;
    }
    else {
        take = Take + _skip;
        nextDisabled = false;
    }

    if (_skip > 0) {
        prevDisabled = false;
    }
    else {
        prevDisabled = true;
    }

    return {
        nextDisabled: nextDisabled,
        prevDisabled: prevDisabled,
        text: {
            from: _skip + 1,
            to: take,
            count: _total
        }
    }
}

export const Service = {
    Get,
    Post,
    Put,
    Delete,
    PostImage,
    ApplyPaging
}