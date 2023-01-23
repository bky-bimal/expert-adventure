variable "region" {
  default = "us-east-1"
}

variable "lambda_name" {
  default = "redtailone-dataexport"
}

variable "s3_bucket_name" {
  default = "dataexportredtailbucket"
}


variable "path" {
  type = string
}