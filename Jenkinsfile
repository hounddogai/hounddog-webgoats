pipeline {
    stages {
        stage('HoundDog.ai') {
            steps {
                sh 'docker run --pull=always -v .:/scanpath hounddogai/scanner:staging hounddog scan'
            }
        }
    }
}
