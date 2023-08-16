const LoginComponent = (props) => {
	const [loginFormData, setLoginFormData] = React.useState({ email: '', password: '' });


	handleEmailChange = (e) => {
		setLoginFormData({
			email: e.target.value,
			password: loginFormData.password
		})
	}

	handlePasswordChange = (e) => {
		setLoginFormData({
			email: loginFormData.email,
			password: e.target.value,
		})
	}

	handleSubmit = e => {
		e.preventDefault();

		console.log(loginFormData.email);
		console.log(loginFormData.password);

		var data = new FormData();
		data.append('email', loginFormData.email);
		data.append('password', loginFormData.password);

		try {
			var xhr = new XMLHttpRequest();
			xhr.open('post', props.submitUrl, true);

			xhr.send(data);
		} catch (error) {
			alert(`${error.message}. Try again`);
		}
	};
	return (
		<div className="container">
			<div className="row min-vh-100">
				<div className="col d-flex flex-column mt-5 align-items-center">
					<form onSubmit={handleSubmit} className="ms-3 pb-2 me-3">
						<h2 className="text-center">Login</h2>
						<label className="d-flex ms-4">
							Email address
						</label>
						<input
							className="ms-4"
							style={{ width: "21rem" }}
							type="email"
							placeholder="Enter email"
							value={loginFormData.email}
							onChange={(e) => handleEmailChange(e)}
						/>
						<label className="d-flex ms-4 mt-2">
							Password
						</label>
						<input
							className="ms-4"
							style={{ width: "21rem" }}
							type="password"
							placeholder="Enter password"
							value={loginFormData.password}
							onChange={(e) => handlePasswordChange(e)}
						/>
						<div className="text-center">
							<button className="ms-3 mt-2 center-block btn-success" style={{ width: "7rem" }} variant="primary" type="submit">Login</button>
						</div>
					</form>
				</div>
			</div>
		</div>
	);
}