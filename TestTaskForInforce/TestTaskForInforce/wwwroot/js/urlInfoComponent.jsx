const UrlInfoComponent = (props) => {
    return (
        <>
            <h1 className="text-center my-3">Short url info</h1>
            <div className="card">
                <div className="card-body">
                    <div><b>Base Url:</b> <a className="text-decoration-none" href={props.data.baseUrl}>{props.data.baseUrl}</a></div>
                    <div><b>Short Url:</b> <a className="text-decoration-none" href={`https://localhost:7068/${props.data.shortenedUrl}`}>{`https://localhost:7068/${props.data.shortenedUrl}`}</a></div>
                    <div><b>Created By:</b> {props.data.user.email}</div>
                    <div><b>Created Date:</b> {props.data.createdAt}</div>
                    <button className="btn btn-primary mt-2" variant="primary" type="submit">
                        <a className="text-decoration-none text-white" href={props.shortUrlsTableUrl}>
                            Back to Short url list
                        </a>
                    </button>
                </div>
            </div>
        </>
    );
}