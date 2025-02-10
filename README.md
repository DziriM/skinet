# Start Terminals to launch the application

- Launch docker desktop in local mode and start the containers
- API -> cd in the API folder and use 'dotnet watch run'
- CLIENT -> cd in the CLIENT folder and use 'ng serve'
- STRIPE -> in local mode, use : stripe listen --forward-to https://localhost:5001/api/payments/webhook -e payment_intent.succeeded
- To create components/services/guards/interceptors use another terminal and cd in the CLIENT folder : ng g c/s/g/i
