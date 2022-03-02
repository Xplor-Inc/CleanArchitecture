export const ToastError = (props:{ errors: string[]}) => {
    return (
        <ul>
            {
                props.errors.map(error => {
                   return <li key={error}>
                        {error}
                    </li>
                })
            }
        </ul>
    )
}
