pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh '''
                    cd src;
                    ls;
                    if [ "$BRANCH_NAME" == "develop" ]; then
                        echo "develop"
						echo "更新开发环境"
                        _host=172.18.9.163
                        _name=cdb
                        docker -H $_host build -t coolyu/com.car-dealer.api:latest --build-arg environment=Development .
                        docker -H $_host stop $_name;
                        docker -H $_host rm $_name;
                        docker -H $_host run -d --restart=on-failure:3 -p 8300:8300 --name $_name coolyu/com.car-dealer.api:latest;
                    elif [ "$BRANCH_NAME" == "release" ]; then
                        echo "release"
						echo "更新测试环境"
                        _host=172.18.11.189
                        _name=cdb
                        docker -H $_host build -t coolyu/com.car-dealer.api:latest --build-arg environment=Staging .
                        docker -H $_host stop $_name;
                        docker -H $_host rm $_name;
                        docker -H $_host run -d --restart=on-failure:3 -p 8300:8300 --name $_name coolyu/com.car-dealer.api:latest;
                    elif [ "$BRANCH_NAME" == "master" ]; then
                        echo "master"
                    fi
                '''
                echo 'Building....'
            }
        }
    }
}