const UrlListComponent = (props) => {
	const [urls, setUrls] = React.useState(props.data);

	loadUrlsFromServer = () => {
		var xhr = new XMLHttpRequest();
		xhr.open('get', props.url, true);
		xhr.onload = function () {
			var data = JSON.parse(xhr.responseText);
			setUrls(data);
		}.bind(this);

		xhr.send();
	};

	handleSubmit = url => {
		var data = new FormData();
		data.append('url', url.url);

		try {
			var xhr = new XMLHttpRequest();
			xhr.open('post', props.submitUrl, true);
			xhr.onload = function () {
				loadUrlsFromServer();
			}.bind(this);

			xhr.send(data);
		} catch (error) {
			alert(`${error.message}. Try again`);
		}
	};

	React.useEffect(() => {
		window.setInterval(loadUrlsFromServer, props.interval);
	}, []);

	return (
		<>
			{props.isAuthenticated && (
				<ShortUrlForm onSubmit={handleSubmit} />
			)}			
			<h1 className="text-center my-5">Url List</h1>
			<div className="table-responsive">
				<table className="table table-striped">
					<thead>
						<tr>
							<th style={{ width: "1%" }} scope="col">Id</th>
							<th style={{ width: "70%" }} scope="col">Base Url</th>
							<th style={{ width: "15%" }} scope="col">Shortened Url</th>
						</tr>
					</thead>
					<tbody>
						{urls.map((url) => (
							<tr key={url.id}>
								<th scope="row">{url.id}</th>
								<td scope="row"><a href={url.baseUrl} target="_blank">{url.baseUrl}</a></td>
								<td scope="row"><a href={`https://localhost:7068/${url.shortenedUrl}`} target="_blank">https://localhost:7068/{url.shortenedUrl}</a></td>
							</tr>
						))}
					</tbody>
				</table>
			</div>
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
			<input type="submit" value="Short url" />
		</form>
	);
}
