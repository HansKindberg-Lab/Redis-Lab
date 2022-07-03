# Redis-Lab

This is a web-application to lab with Redis. Redis configuration and persistent data etc.

![.github/workflows/Docker-deploy.yml](https://github.com/HansKindberg-Lab/Redis-Lab/actions/workflows/Docker-deploy.yml/badge.svg)

Web-application, without configuration, pushed to Docker Hub: https://hub.docker.com/r/hanskindberg/redis-lab

## 1 Example

### 1.1 Without data-persistence

#### 1.1.1

Start Redis locally with Docker.

	docker run -it --name "redis" -p 6379:6379 --rm redis --requirepass "P@ssword12"

- -it - interactive and tty
- --name "redis" - the container is named "redis"
- -p 6379:6379 - port mapping
- redis - the image used
- --requirepass "P@ssword12" - requires redis authentication with the password

You can now connect to redis with the following connection-string: "localhost:6379,password=P@ssword12"

#### 1.1.2 Run this solution with Kestrel

Add some keys.

#### 1.1.3 Stop Redis

Stop Redis and start it again.

	docker run -it --name "redis" -p 6379:6379 --rm redis --requirepass "P@ssword12"

#### 1.1.4 Refresh the home-page

There are now no items in Redis.

### 1.2 With data-persistence

#### 1.2.1

Start Redis locally with Docker.

	docker run -it --name "redis" -p 6379:6379 --rm -v "C:\Data\Docker\Redis\Data":"/data" redis --requirepass "P@ssword12"

- -it - interactive and tty
- --name "redis" - the container is named "redis"
- -p 6379:6379 - port mapping
- -v "C:\Data\Docker\Redis\Data":"/data" - the local directory "C:\Data\Docker\Redis\Data" is mounted as redis data directory, this gives us persisted data when we restart the container
- redis - the image used
- --requirepass "P@ssword12" - requires redis authentication with the password

You can now connect to redis with the following connection-string: "localhost:6379,password=P@ssword12"

#### 1.2.2 Run this solution with Kestrel

Add some keys.

#### 1.2.3 Stop Redis

Stop Redis and start it again.

	docker run -it --name "redis" -p 6379:6379 --rm -v "C:\Data\Docker\Redis\Data":"/data" redis --requirepass "P@ssword12"

#### 1.2.4 Refresh the home-page

The items from Redis are still there.

## 2 Redis

### 2.1 Simple

	docker run redis

### 2.2 Name the container

	docker run --name "redis" redis

### 2.3 Configure port

	docker run -p 6379:6379 redis

### 2.4 Interactive and tty

	docker run -it redis

- -i, --interactive
- -t, --tty
- -it, both above
- [Option types](https://docs.docker.com/engine/reference/commandline/exec/#options)

### 2.5 Clean up (--rm)

	docker run --rm redis

## 3 Configuration example for forwarded headers

	{
		"ForwardedHeaders": {
			"ForwardedHeaders": "All"
		}
	}

## 4 Links

- [Docker run reference](https://docs.docker.com/engine/reference/run/)
- [Docker: Option types](https://docs.docker.com/engine/reference/commandline/cli/#option-types)
- [Use the Docker command line](https://docs.docker.com/engine/reference/commandline/cli/)