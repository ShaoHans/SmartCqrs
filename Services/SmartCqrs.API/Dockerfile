#ÿ��FROM��������һ������

#######################################################################
#ָ������asp.net coreӦ�ó�����Ҫ�ľ���
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

#���嵱ǰ����ı�����������build�����ʱ�����ͨ��[--build-arg environment=Development]������ֵ
ARG environment

#��asp.net coreӦ�ó�����Ҫ�õ��Ļ���������ֵ��������Ӧ�ó���������ʱ��ͻ������Ӧ�������ļ���appsettings.{env}.json
ENV ASPNETCORE_ENVIRONMENT=$environment
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

#��¶����˿ں�
EXPOSE 8800

#######################################################################
#ָ������asp.net coreӦ�ó�����Ҫ�ľ���
FROM microsoft/dotnet:2.1-sdk AS build

#ָ����ǰ����Ĺ���Ŀ¼���������������Զ�����
WORKDIR /src

#����������ǰĿ¼���е��ļ����ļ��п����������WORKDIRĿ¼��
#��������.������ʾ��ǰ������
#��һ����.���������������������ģ�Ҳ���ǽ���������ڵĸ�Ŀ¼�������ڹ�������ʱ��ѽ������������Դ���뿽���������н��б��롢�����Ȳ�����
#һ�����ǵĽ���������кü�����Ŀ��n�������Ŀ��m��asp.net core��Ŀ������Dockerfile���Ƕ����ڸ��Ե�asp.net core��Ŀ�ĸ�Ŀ¼�У�����
#��build�����ʱ����ֱ����asp.net core��Ŀ�ĸ�Ŀ¼��ִ��docker build����������Բ���ָ��Dockerfile��·����docker��Ĭ���ڵ�ǰĿ¼
#����Dockerfile�ļ���������ֻ���asp.net core��Ŀ��Դ���뿽���������src�У��������������������Դ���룬����build�϶����������ȷ�������ǣ�
#�򿪽���������ڵĸ�Ŀ¼����build��ʱ��ͨ��-f����ָ��Dockerfile�ļ�·���������Ż���������������Դ���뿽���������srcĿ¼�С�
#�ڶ�����.���������������������ģ�Ҳ������WORKDIRָ���ĵ�ǰ����Ŀ¼
COPY . .

#�ѹ���Ŀ¼�л���asp.net core��Ŀ���ڵ�Ŀ¼�����л�ԭ���������
WORKDIR /src/Services/SmartCqrs.API
#��ԭ
RUN dotnet restore "SmartCqrs.API.csproj"
#����
RUN dotnet build "SmartCqrs.API.csproj" 

#######################################################################
#�������񣬻�����һ���Ĳ������뾵��
FROM build AS publish
#��������asp.net coreӦ�ó������dll����Դ�ļ�����������outputĿ¼
RUN dotnet publish "SmartCqrs.API.csproj" -o /output

#######################################################################
#���վ���
FROM base AS final
WORKDIR /app
#�ӷ��������outputĿ¼�аѳ����ļ����������վ���Ĺ���Ŀ¼��
COPY --from=publish /output .
#ָ����������asp.net core���������
ENTRYPOINT ["dotnet", "SmartCqrs.API.dll"]

#######################################################################
#������������ͨ��-fָ��Dockerfile�ļ�·��
#docker build -t smartcqrs:1.0 -f Services/SmartCqrs.API/Dockerfile --build-arg environment=Development .