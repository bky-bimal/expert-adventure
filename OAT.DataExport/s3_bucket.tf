resource "aws_s3_bucket" "lambda_bucket" {
  bucket = var.s3_bucket_name
}



resource "aws_s3_bucket_object" "example" {
  bucket = var.s3_bucket_name
  key    = "1.zip"
  source = var.path
}

resource "aws_s3_bucket_policy" "lambda_bucket_policy" {
  bucket = aws_s3_bucket.lambda_bucket.bucket
  policy = <<EOF
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "AddPerm",
            "Effect": "Allow",
            "Principal": "*",
            "Action": "s3:GetObject",
            "Resource": "${aws_s3_bucket.lambda_bucket.arn}/*"
        }
    ]
}
EOF
}