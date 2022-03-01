export interface IResult<T> {
    resultObject: T
    hasErrors: boolean
    errors: string[]
    rowCount:number
}