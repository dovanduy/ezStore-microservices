istioctl kube-inject -f k8s\k8s.service.api.yaml -o k8s\k8s.service.api.istio.yaml
kubectl apply -f k8s\k8s.service.api.istio.yaml

REM kubectl -n istio-system port-forward istio-egressgateway-56bdd5fcfb-rxbzs 30001
REM kubectl -n istio-system port-forward istio-egressgateway-56bdd5fcfb-rxbzs 30002
