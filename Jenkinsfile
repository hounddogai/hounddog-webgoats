pipeline {
    agent any
    stages {
        stage('HoundDog.ai') {
            steps {
                docker run --pull=always -v .:/scanpath hounddogai/scanner:staging hounddog scan
            }
        }
    }
}
