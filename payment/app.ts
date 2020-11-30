import {
  AmqpConnectOptions,
  connect,
} from "https://deno.land/x/amqp@v0.14.0/mod.ts";
import "https://deno.land/x/dotenv/load.ts";

import { Payment } from "./src/types/payment.ts";
import { createPayment } from "./src/services/PaymentService.ts";

const port = Deno.env.get("RABBITMQ_DEFAULT_PORT");

const RABBITMQ_DEFAULT_VHOST = Deno.env.get("RABBITMQ_DEFAULT_VHOST");
const RABBITMQ_DEFAULT_HOST = Deno.env.get("RABBITMQ_DEFAULT_HOST");
const RABBITMQ_DEFAULT_PORT = port ? parseInt(port) : undefined;
const RABBITMQ_DEFAULT_USER = Deno.env.get("RABBITMQ_DEFAULT_USER");
const RABBITMQ_DEFAULT_PASS = Deno.env.get("RABBITMQ_DEFAULT_PASS");
const RABBITMQ_CONSUMER_QUEUE_NAME = Deno.env.get(
  "RABBITMQ_CONSUMER_QUEUE_NAME",
);

const opt = {
  vhost: RABBITMQ_DEFAULT_VHOST,
  hostname: RABBITMQ_DEFAULT_HOST,
  port: RABBITMQ_DEFAULT_PORT,
  username: RABBITMQ_DEFAULT_USER,
  password: RABBITMQ_DEFAULT_PASS,
} as AmqpConnectOptions;

const connection = await connect(opt);
const channel = await connection.openChannel();

await channel.consume(
  { queue: RABBITMQ_CONSUMER_QUEUE_NAME },
  async (args, props, data) => {
    try {
      const payment = JSON.parse(new TextDecoder().decode(data)) as Payment;

      await createPayment(payment);

      await channel.ack({ deliveryTag: args.deliveryTag });
    } catch (error) {
      await channel.nack(
        { deliveryTag: args.deliveryTag, multiple: false, requeue: false },
      );
    }
  },
);
