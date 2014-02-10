module.exports = function(grunt) {
	var sourceDirectory = "src/SevenDigital.Api.Feeds.Filtered/",  //NB - Only putting web service in gruntfile for now, look to doing rest of it later
		deployment = "deployment-package/",
		privateKey = grunt.file.read("C:\\TeamCityBuildTools\\DeploymentKeyConfig\\Key_openssh.ppk");

	// Project configuration
	grunt.initConfig({
		dirs: {
			deployment: "7d-feeds-filtered"
		},
		clean: {
			release: [deployment]
		},
		copy: {
			main: {
				files: [
					{ cwd: sourceDirectory + '/bin/', src: '*.dll', dest: deployment + 'bin/', expand: true },
					{ cwd: sourceDirectory + '/', src: 'Web.config', dest: deployment, expand: true },
					{ cwd: sourceDirectory + '/', src: 'Global.asax', dest: deployment, expand: true },
					{ cwd: sourceDirectory + '/', src: 'credentials.txt', dest: deployment, expand: true },
				]
			}
		},
		zip: {
			"deployment-package.zip": ['deployment-package/**/*']
		},
		sshconfig: {
			"uat": {
				host: "prod-toolapp01.win.sys.7d",
				privateKey: privateKey,
				username: "sshduser",
				path: "/cygdrive/c/inetpub/<%= dirs.deployment %>/uat_deploy/",
			},
			"prod": {
				host: "prod-toolapp01.win.sys.7d",
				privateKey: privateKey,
				username: "sshduser",
				path: "/cygdrive/c/inetpub/<%= dirs.deployment %>/prod_deploy/",
			},
		},
		sftp: {
			deploy: {
				files: {
					"./": "*.zip"
				},
				options: {
					createDirectories: true
				}
			}
		},
		sshexec: {
			clean: {
				command: "cd <%= sshconfig[\"" + grunt.option('config') + "\"].path %> ; pwd; rm -rf deployment-package",
			},
			unpack: {
				command: "cd <%= sshconfig[\"" + grunt.option('config') + "\"].path %> ; pwd; /cygdrive/c/7zip/7z.exe x *.zip -o./ -y;",
			},
			rsync: {
				command: "cd <%= sshconfig[\"" + grunt.option('config') + "\"].path %> ; pwd; rsync --delete -r deployment-package/ ../" + grunt.option('config') + "; chmod -R 755 ../" + grunt.option('config')
			}
		}
	});

	// Load grunt plugins and tasks
	grunt.loadNpmTasks("grunt-contrib-clean");
	grunt.loadNpmTasks("grunt-contrib-copy");
	grunt.loadNpmTasks('grunt-zip');
	grunt.loadNpmTasks('grunt-ssh');

	// Run default tasks
	grunt.registerTask("default", ["copy"]);
	grunt.registerTask("build", "Compiles all of the assets and copies the files to 'deployment-package.zip'.", ["clean", "copy", "zip", "clean"]);
	grunt.registerTask("deploy", 'Deploys to location specified by --config {env_name}', ["sftp:deploy", "sshexec"]); // needs --config=prod
	grunt.registerTask("build-deploy", 'Builds and deploys to location specified by --config {env_name}', ["build", "deploy"]);
};