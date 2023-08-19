const UrlListComponent = (props) => {
	const [urls, setUrls] = React.useState(props.data);
	const [deleleResponse, setDeleleResponse] = React.useState();
	const [addResponse, setAddResponse] = React.useState();

	React.useEffect(() => {
		if (addResponse) {
			if (addResponse.id) {
				alert(`Shortened url has been added with id = ${addResponse.id}`);
				loadUrlsFromServer();
			}
			if (addResponse.message) {
				alert(addResponse.message);
			}
		}

	}, [addResponse]);

	React.useEffect(() => {
		if (deleleResponse) {
			if (deleleResponse.isDeleted) {
				alert("Url has been removed");
				loadUrlsFromServer();
			}
			else {
				alert("Failed to remove");
			}
		}

	}, [deleleResponse]);

	loadUrlsFromServer = () => {

		var xhr = new XMLHttpRequest();
		xhr.open('get', props.url, true);
		xhr.onload = function () {
			var data = JSON.parse(xhr.responseText);
			setUrls(data);
		}.bind(this);

		xhr.send();
	};

	handleAddSubmit = url => {
		const requestOptions = {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({ url: url.url })
		};

		(async () => {
			setAddResponse(
				await fetch(props.addUrl, requestOptions).then(e => e.json()));
		})();
	};

	handleDeleteSubmit = (id) => {
		const requestOptions = {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' }
		};

		(async () => {
			setDeleleResponse(
				await fetch(`${props.deleteUrl}/${id}`, requestOptions).then(e => e.json()));
		})();
	}

	return (
		<>
			{props.isAuthenticated && (
				<ShortUrlForm onSubmit={handleAddSubmit} />
			)}
			<h1 className="text-center my-3">Url List</h1>
			{urls.length > 0 ? <div className="table-responsive">
				<table className="table table-striped">
					<thead>
						<tr>
							<th style={{ width: "50%" }} scope="col">Base Url</th>
							<th style={{ width: "25%" }} scope="col">Shortened Url</th>
							<th style={{ width: "15%" }} scope="col"></th>
							<th style={{ width: "10%" }} scope="col"></th>
						</tr>
					</thead>
					<tbody>
						{urls.map((url) => (
							<tr key={url.id}>
								<td scope="row"><a className="text-decoration-none" href={url.baseUrl} target="_blank">{url.baseUrl}</a></td>
								<td scope="row"><a className="text-decoration-none" href={`https://localhost:7068/${url.shortenedUrl}`} target="_blank">https://localhost:7068/{url.shortenedUrl}</a></td>
								<td scope="row">
									<button className="btn btn-success" variant="primary" type="submit">
										<a className="text-decoration-none text-white" href={`${props.showInfoUrl}/${url.id}`}>
										Show details
									</a>
									</button>
									</td>
								{
									(props.isAdmin || url.user.email === props.userEmail) ? (
										<td scope="row">
											<button className="btn btn-danger" style={{ width: "7rem" }} variant="primary" type="submit" onClick={e => handleDeleteSubmit(url.id)}>Delete</button>
										</td>
									) : (<td scope="row">
										<button className="btn btn-danger" style={{ width: "7rem" }} variant="primary" type="submit" disabled>Delete</button>
									</td>)}
							</tr>
						))}
					</tbody>
				</table>
			</div>
				: <div className="text-center">Url list is empty</div>}
		</>
	);
}

const ShortUrlForm = (props) => {
	const [createShortenedUrlformData, setCreateShortenedUrlformData] = React.useState({ url: '' });

	handleChange = (e) => {
		setCreateShortenedUrlformData({ url: e.target.value })
	}

	handleSubmit = e => {
		e.preventDefault();

		var url = createShortenedUrlformData.url.trim();

		if (!url) {
			return;
		}

		props.onSubmit({ url: url });
		setCreateShortenedUrlformData({ url: '' });
	};

	return (
		<form onSubmit={handleSubmit}>
			<input
				type="text"
				placeholder="Enter url"
				value={createShortenedUrlformData.url}
				onChange={handleChange}
			/>
			<input className="ms-2" type="submit" value="Short url" />
		</form>
	);
}
