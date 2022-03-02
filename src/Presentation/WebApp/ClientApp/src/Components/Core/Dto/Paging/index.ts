export interface IPagingResult {
    nextDisabled: boolean,
    prevDisabled: boolean,
    text: PagingText,
    prevPage:React.MouseEventHandler<HTMLButtonElement>,
    nextPage:React.MouseEventHandler<HTMLButtonElement>
}

interface PagingText {
    from: number,
    to: number,
    count: number
}