#每次FROM都会生成一个镜像

#######################################################################
#指定运行asp.net core应用程序需要的镜像
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

#定义当前镜像的变量参数，在build镜像的时候可以通过[--build-arg environment=Development]参数赋值
ARG environment

#给asp.net core应用程序需要用到的环境变量赋值，这样在应用程序启动的时候就会加载相应的配置文件：appsettings.{env}.json
ENV ASPNETCORE_ENVIRONMENT=$environment
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

#暴露镜像端口号
EXPOSE 8800

#######################################################################
#指定编译asp.net core应用程序需要的镜像
FROM microsoft/dotnet:2.1-sdk AS build

#指定当前镜像的工作目录，如果不存在则会自动创建
WORKDIR /src

#把宿主机当前目录所有的文件和文件夹拷贝到镜像的WORKDIR目录中
#这两个“.”都表示当前上下文
#第一个“.”代表的是宿主机上下文，也就是解决方案所在的根目录，我们在构建镜像时会把解决方案的所有源代码拷贝到镜像中进行编译、发布等操作。
#一般我们的解决方案会有好几个项目（n个类库项目、m个asp.net core项目），而Dockerfile都是定义在各自的asp.net core项目的根目录中，有人
#在build镜像的时候都是直接在asp.net core项目的根目录中执行docker build命令，这样可以不用指定Dockerfile的路径，docker会默认在当前目录
#查找Dockerfile文件，这样就只会把asp.net core项目的源代码拷贝到镜像的src中，并不是整个解决方案的源代码，这样build肯定会出错。正确的做法是，
#打开解决方案所在的根目录，在build的时候通过-f参数指定Dockerfile文件路径，这样才会把整个解决方案的源代码拷贝到镜像的src目录中。
#第二个“.”代表的是容器的上下文，也就是由WORKDIR指定的当前工作目录
COPY . .

#把工作目录切换到asp.net core项目所在的目录，进行还原、编译操作
WORKDIR /src/Services/SmartCqrs.API
#还原
RUN dotnet restore "SmartCqrs.API.csproj"
#编译
RUN dotnet build "SmartCqrs.API.csproj" 

#######################################################################
#发布镜像，基于上一步的产生编译镜像
FROM build AS publish
#发布，把asp.net core应用程序相关dll、资源文件输出到镜像的output目录
RUN dotnet publish "SmartCqrs.API.csproj" -o /output

#######################################################################
#最终镜像
FROM base AS final
WORKDIR /app
#从发布镜像的output目录中把程序文件拷贝到最终镜像的工作目录中
COPY --from=publish /output .
#指定用于运行asp.net core程序的命令
ENTRYPOINT ["dotnet", "SmartCqrs.API.dll"]

#######################################################################
#构建镜像的命令，通过-f指定Dockerfile文件路径
#docker build -t smartcqrs:1.0 -f Services/SmartCqrs.API/Dockerfile --build-arg environment=Development .