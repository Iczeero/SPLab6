package npi.sp.lab6;

import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.Socket;

public class StudentActivity extends AppCompatActivity {

    private EditText editTextStudentId;
    private Button buttonSend;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student); // Проверьте наличие этого файла разметки

        editTextStudentId = findViewById(R.id.editTextStudentId);
        buttonSend = findViewById(R.id.buttonSend);

        buttonSend.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String studentId = editTextStudentId.getText().toString();
                if (!studentId.isEmpty()) {
                    sendStudentStatus(studentId);
                } else {
                    Toast.makeText(StudentActivity.this, "Введите ID студента", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }

    private void sendStudentStatus(String studentId) {
        new SendStudentStatusTask(this).execute(studentId);
    }

    private static class SendStudentStatusTask extends AsyncTask<String, Void, String> {
        private final StudentActivity activity;

        // Конструктор для передачи контекста Activity
        public SendStudentStatusTask(StudentActivity activity) {
            this.activity = activity;
        }

        @Override
        protected String doInBackground(String... params) {
            String studentId = params[0];
            String responseMessage = "";

            try {
                // Подключение к серверу
                Socket socket = new Socket("192.168.0.105", 8888); // Замените на IP вашего сервера
                OutputStream outputStream = socket.getOutputStream();
                outputStream.write(studentId.getBytes());
                outputStream.flush();
                outputStream.close();
                socket.close();
            } catch (Exception e) {
                e.printStackTrace();
                responseMessage = "Ошибка при соединении с сервером";
            }

            return responseMessage;
        }

        @Override
        protected void onPostExecute(String result) {
            if (result != null && !result.isEmpty()) {
                Toast.makeText(activity, result, Toast.LENGTH_SHORT).show();
            } else {
                Toast.makeText(activity, "Статус студента отправлен", Toast.LENGTH_SHORT).show();
            }
        }
    }
}