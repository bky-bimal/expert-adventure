resource "aws_lambda_function" "lambda_function" {
  depends_on = [
    aws_s3_bucket_object.example
  ]
  function_name = var.lambda_name
  role =  aws_iam_role.lambda_role.arn
  handler = "OAT.DataExport::OAT.DataExport.Function::FunctionHandler"
  runtime = "dotnet6"
  s3_bucket = var.s3_bucket_name
  s3_key = "1.zip"
}


output "lambda_arn" {
  value = aws_lambda_function.lambda_function.arn
}
