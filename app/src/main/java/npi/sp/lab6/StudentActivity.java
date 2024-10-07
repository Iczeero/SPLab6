package npi.sp.lab6;

import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.lang.ref.WeakReference;
import java.net.Socket;

public class StudentActivity extends AppCompatActivity {

    private EditText editTextIpAddress;
    private Button buttonSend;
    private TextView textViewStudentInfo;
    private SharedPreferences sharedPreferences;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_student);


        sharedPreferences = getSharedPreferences("StudentData", MODE_PRIVATE);

        textViewStudentInfo = findViewById(R.id.textViewStudentInfo);
        editTextIpAddress = findViewById(R.id.editTextIpAddress);
        buttonSend = findViewById(R.id.buttonSend);


        String name = sharedPreferences.getString("Name", "Имя не установлено");
        String studentId = sharedPreferences.getString("StudentId", "ID не установлен");


        String studentInfo = "Имя: " + name + "\nID: " + studentId;
        textViewStudentInfo.setText(studentInfo);

        buttonSend.setOnClickListener(v -> {
            String ipAddress = editTextIpAddress.getText().toString();

            if (!ipAddress.isEmpty()) {

                if ("ID не установлен".equals(studentId)) {
                    Toast.makeText(StudentActivity.this, "Сначала зарегистрируйтесь", Toast.LENGTH_SHORT).show();
                } else {

                    new SendStudentStatusTask(this).execute(ipAddress, studentId);
                }
            } else {
                Toast.makeText(StudentActivity.this, "Введите IP-адрес", Toast.LENGTH_SHORT).show();
            }
        });
    }

    private static class SendStudentStatusTask extends AsyncTask<String, Void, String> {
        private final WeakReference<StudentActivity> activityReference;

        public SendStudentStatusTask(StudentActivity activity) {
            activityReference = new WeakReference<>(activity);
        }

        @Override
        protected String doInBackground(String... params) {
            String ipAddress = params[0];
            String studentId = params[1];
            String responseMessage = "";

            try (Socket socket = new Socket(ipAddress, 8888);
                 OutputStream outputStream = socket.getOutputStream();
                 BufferedReader input = new BufferedReader(new InputStreamReader(socket.getInputStream()))) {


                outputStream.write((studentId + "\n").getBytes());
                outputStream.flush();


            } catch (IOException e) {
                e.printStackTrace();
                responseMessage = "Ошибка при соединении с сервером";
            }

            return responseMessage;
        }

        @Override
        protected void onPostExecute(String result) {
            StudentActivity activity = activityReference.get();
            if (activity == null || activity.isFinishing()) return;

            if (result != null && !result.isEmpty()) {
                Toast.makeText(activity, result, Toast.LENGTH_SHORT).show();
            } else {
                Toast.makeText(activity, "Статус студента отправлен", Toast.LENGTH_SHORT).show();
            }
        }
    }
}